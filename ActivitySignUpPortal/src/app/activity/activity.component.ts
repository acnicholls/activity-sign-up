import { ActivitySignedUpViewModel } from './../ActivitySignedUpViewModel';
import { ActivityModel } from './../ActivityModel';
import { Component, OnInit, OnDestroy } from '@angular/core';
// import { CookieService } from 'ngx-cookie';
import { ActivatedRoute } from '@angular/router';
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

  private searchTerm = 'activityId';



  constructor(
    private dataAccessService: DataAccessService,
    // private cookieService: CookieService,
    private routeService: ActivatedRoute
    ) {     }

  ngOnInit(): void {
      // parse the cookie content for the activity Id
      const routeId  = this.routeService.snapshot.data[this.searchTerm];
      // if it exists, show the participant list/ comment list
      // const cookieContent = this.cookieService.get(routeId.toString());

      // if (!(cookieContent == null) && cookieContent === 'true')
      if (false)
      {
        this.dataAccessService.getSignedUpActivity(routeId).subscribe(
            (returnValue) => {
                this.userActivity = returnValue;
            },
            error => {
              console.log(error);
            }
          );
        }
        else{
          // if it doesn't show the sign up view
          this.dataAccessService.getActivity(routeId).subscribe(
            (returnValue) => {
                this.activity = returnValue;
            },
            error => {
              console.log(error);
            }
          );
        }
  }

}
