import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ActivityInsertModel } from '../../../models/activity/ActivityInsertModel';
import { DataAccessService } from '../../../services/data-access.service';

@Component({
  selector: 'app-new-activity-form',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.sass'],
  providers: []
})
export class NewActivityComponent implements OnInit {

  constructor(private dataAccessService: DataAccessService, private router: Router) { }

  model = new ActivityInsertModel('', '', new Date(), '') ;
  submitted = false;

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted = true;
    this.dataAccessService.createNewActivity(this.model).subscribe(
      (data) => {
        this.router.navigateByUrl('/activity/' + data);
      },
      error => {
        console.log(error);
      }
    );
  }
}