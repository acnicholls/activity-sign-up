import { ActivityPersonModel } from './../../models/activity/ActivityPersonModel';
import { CommentInsertModel } from '../../models/comment/CommentInsertModel';
import { PersonInsertModel } from '../../models/person/PesonInsertModel';
import { ActivitySignedUpViewModel } from '../../models/activity/ActivitySignedUpViewModel';
import { ActivityModel } from '../../models/activity/ActivityModel';
import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ActivatedRoute } from '@angular/router';
import { DataAccessService} from '../../services/data-access.service';
import { Subscription } from 'rxjs';

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

  userActivities: Array<ActivityPersonModel> = new Array<ActivityPersonModel>();

  localStorageKey: string = 'activitySignUpPortalData';

  currentCommentor: number = 0;

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

    // load the localStorage for this 'user'
    if(this.getLocalStorageData())
    {
      //  check to see if this user has registered for this activity
      this.userSignedUp = this.userActivities.find(ap => ap.activityId == this.routeId) != undefined;
    }
    if (this.userSignedUp)
    {
      // get the activity/person record for this user
      var activityPerson = this.userActivities.find(ap => ap.activityId == this.routeId);
      
      this.dataAccessService.getSignedUpActivity(this.routeId).subscribe(
        (returnValue) => {
          this.userActivity = new ActivitySignedUpViewModel().deserialize(returnValue);
          this.commentModel.CommentActivityId = this.routeId;
          if(activityPerson?.activityPersonId)
          {
            this.currentCommentor = activityPerson.activityPersonId;
            this.commentModel.CommentPersonId = this.currentCommentor;
          }
        },
        (error) => {
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
        (error) => {
          console.log(error);
        }
      );
    }
  }

  getLocalStorageData(): boolean {
    // get user activity list from local storage
    let jsonString = localStorage.getItem(this.localStorageKey);
    if(jsonString)
    {
      this.userActivities = JSON.parse(jsonString);
      return true;
    }
    return false;
  }

  setLocalStorageData() : void {
    localStorage.setItem(this.localStorageKey, JSON.stringify(this.userActivities));
  }

  onSubmit(): void  {
    this.submitted = true;
    // here we save the participant data to the model and post it to the API, then if successful, reload the component
    this.dataAccessService.createNewPerson(this.model).subscribe(
      (userId) => {
        // create a new activity/person object
        let signupObject = new ActivityPersonModel(this.routeId, parseInt(userId.toString()));
        // add it to local storage
        this.getLocalStorageData();
        this.userActivities.push(signupObject);
        this.setLocalStorageData();
        // reload the page so that the user can see the signed up
        this.ngOnInit();
      },
      (error) => {
        console.log(error);
      }
    )
  }

  onCommentSubmit(): void {
    this.dataAccessService.createNewComment(this.commentModel).subscribe(
      () => {
        this.commentModel.CommentContent = '';
        this.ngOnInit();
      }
    )
  }
}