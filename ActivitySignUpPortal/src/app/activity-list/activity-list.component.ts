import { Component, OnInit } from '@angular/core';

import {ActivityListModel} from '../ActivityListModel';
import { DataAccessService} from '../services/data-access.service';
import { MapType } from '@angular/compiler';

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
