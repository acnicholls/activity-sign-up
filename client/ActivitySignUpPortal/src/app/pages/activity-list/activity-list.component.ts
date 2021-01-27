import { Component, OnInit } from '@angular/core';
import { ActivityListModel } from '../../models/activity/ActivityListModel';
import { DataAccessService } from '../../services/data-access.service';

@Component({
  selector: 'app-activity-list',
  templateUrl: './activity-list.component.html',
  styleUrls: ['./activity-list.component.sass']
})
export class ActivityListComponent implements OnInit {

  activityList = new Array<ActivityListModel>();

  constructor(private dataAccessService: DataAccessService) {

   }

  ngOnInit(): void {
     this.dataAccessService.getActivityList().subscribe(
        (returnValue) => {
          returnValue.forEach(activity => {
            this.activityList.push(activity);
          });
        },
        error => {
          console.log(error);
        }
      );
  }
}
