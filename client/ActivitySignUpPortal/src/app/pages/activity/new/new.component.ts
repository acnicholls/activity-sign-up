import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivityImageUploadResponseModel } from '../../../models/activity/ActivityImageUploadResponseModel';
import { ActivityInsertModel } from '../../../models/activity/ActivityInsertModel';
import { DataAccessService } from '../../../services/data-access.service';

@Component({
  selector: 'app-new-activity-form',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.sass'],
  providers: []
})
export class NewActivityComponent implements OnInit {

  model : ActivityInsertModel = new ActivityInsertModel('', '', new Date()) ;
  fileSubmitted : boolean = false;
  response: ActivityImageUploadResponseModel = new ActivityImageUploadResponseModel('');
  

  constructor(private dataAccessService: DataAccessService, private router: Router) { }

  ngOnInit(): void {
  }

  uploadFinished = (event: any) => {
    this.response.ImagePath = event.imagePath;
    this.fileSubmitted = true;
  }

  onSubmit(): void {
    this.model.ActivityImage = this.response.ImagePath;
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