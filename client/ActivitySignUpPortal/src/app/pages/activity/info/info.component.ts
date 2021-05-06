import { ActivityModel } from './../../../models/activity/ActivityModel';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.sass']
})
export class InfoComponent implements OnInit {

  @Input() activity: ActivityModel = new ActivityModel();

  constructor() {
   }

  ngOnInit(): void {
  }

}
