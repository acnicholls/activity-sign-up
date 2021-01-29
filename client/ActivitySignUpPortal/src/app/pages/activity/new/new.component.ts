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

  model : ActivityInsertModel = new ActivityInsertModel('', '', new Date(), '') ;
  submitted : boolean = false;

  constructor(private dataAccessService: DataAccessService, private router: Router) { }

  ngOnInit(): void {
  }

  onFileChange(event: any) {
    const reader = new FileReader();

    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.model.ActivityImage = reader.result as string;
      };
    }
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