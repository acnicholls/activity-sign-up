import { CommentInsertModel } from '../../models/comment/CommentInsertModel';
import { PersonInsertModel } from '../../models/person/PesonInsertModel';
import { ActivitySignedUpViewModel } from '../../models/activity/ActivitySignedUpViewModel';
import { ActivityModel } from '../../models/activity/ActivityModel';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ActivatedRoute } from '@angular/router';
import { DataAccessService} from '../../services/data-access.service';
import { Subscription, Observable } from 'rxjs';
import { Globals } from '../../globals';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.sass']
})
export class ActivityComponent implements OnInit {

  routeSub = new Subscription();

  activity = new ActivityModel();

  userActivity = new ActivitySignedUpViewModel();

  model = new PersonInsertModel("","","", 0);

  userSignedUp: boolean = false;

  submitted: boolean = false;  

  routeId: number = 0;

  cookieName: string = '';

  cookieValue: string = '';

  commentModel = new CommentInsertModel(0,0,"");

  constructor(
    private dataAccessService: DataAccessService,
    private routeService: ActivatedRoute,
    private cookieService: CookieService
    ) {     }

  ngOnInit(): void {
    // get the Id of the activity that we're looking for from the route.
    this.routeService.paramMap.subscribe(params => {
      this.routeId = Number(params.get('activityId'));
    })

    // check the user cookie to see if they have this.routeId
    // if it exists, show the participant list/ comment list
    this.cookieName = `${Globals.COOKIE_NAME}${this.routeId}`;
    this.cookieValue = this.cookieService.get(this.cookieName);
    
    this.userSignedUp = this.cookieValue == '' ? false : true;

    if (this.userSignedUp)
    {
      this.dataAccessService.getSignedUpActivity(this.routeId).subscribe(
        (returnValue) => {
          this.userActivity = new ActivitySignedUpViewModel().deserialize(returnValue);
          this.commentModel.CommentActivityId = this.routeId;
          this.commentModel.CommentPersonId = parseInt(this.cookieValue);
        },
        error => {
          console.log(error);
        }
      );
    }
    else
    {
      // if it doesn't show the sign up view
      this.model = new PersonInsertModel("","","",this.routeId);
      this.dataAccessService.getActivity(this.routeId).subscribe(
        (returnValue) => {
          this.activity = returnValue;
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  onSubmit(): void  {
    this.submitted = true;
    // here we save the participant data to the model and post it to the API, then if successful, reload the component
    this.dataAccessService.createNewPerson(this.model).subscribe(
      (userId) => {
        this.cookieService.set(this.cookieName, userId.toString(), 365);
        this.ngOnInit();
      },
      (error) => {
        console.log(error);
      }
    )
  }

  onCommentSubmit(): void {
    this.commentModel.CommentPersonId = parseInt(this.cookieService.get(this.cookieName)) 
    this.dataAccessService.createNewComment(this.commentModel).subscribe(
      () => {
        this.ngOnInit();
      }
    )
  }
}