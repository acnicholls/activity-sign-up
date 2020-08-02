import { PersonInsertModel } from './../PesonInsertModel';
import { ActivitySignedUpViewModel } from './../ActivitySignedUpViewModel';
import { ActivityModel } from './../ActivityModel';
import { Component, OnInit, OnDestroy } from '@angular/core';
// import { CookieService } from 'ngx-cookie';
import { ActivatedRoute, Router } from '@angular/router';
import { DataAccessService} from '../services/data-access.service';
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

  model = new PersonInsertModel("", "","", 0);

  userSignedUp: boolean = false;

  submitted: boolean = false;  

  routeId: number = 0;

  constructor(
    private dataAccessService: DataAccessService,
    // private cookieService: CookieService,
    private routeService: ActivatedRoute,
    private router: Router
    ) {     }

  ngOnInit(): void {
      // get the Id of the activity that we're looking for from the route.
      this.routeService.paramMap.subscribe(params => {
        this.routeId = Number(params.get('activityId'));
      })

      // check the user cookie to see if they have this.routeId
      // if it exists, show the participant list/ comment list
      // const cookieContent = this.cookieService.get(routeId.toString());

      // if (!(cookieContent == null) && cookieContent === 'true')
      if (false)
      {
        this.dataAccessService.getSignedUpActivity(this.routeId).subscribe(
            (returnValue) => {
                this.userActivity = returnValue;
                this.userSignedUp = true;
            },
            error => {
              console.log(error);
            }
          );
      }
      else
      {
        // if it doesn't show the sign up view
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
    // here we save the participant data to the model and post it to the API
    this.dataAccessService.createNewPerson(this.model).subscribe(
      () => {
        // TODO: save the activityId value to a cookie 
        this.router.navigateByUrl('/activity/' + this.routeId);
      },
      (error) => {
        console.log(error);
      }
    )
  }

}
