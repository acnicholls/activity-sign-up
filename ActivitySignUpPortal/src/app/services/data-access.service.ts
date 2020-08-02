import { CommentInsertModel } from './../CommentInsertModel';
import { PersonInsertModel } from './../PesonInsertModel';
import { ActivitySignedUpViewModel } from './../ActivitySignedUpViewModel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../globals';
import { ActivityInsertModel } from '../ActivityInsertModel';
import { ActivityListModel } from '../ActivityListModel';
import { ActivityModel } from '../ActivityModel';

@Injectable({
  providedIn: 'root'
})
export class DataAccessService {

  constructor(private client: HttpClient) {   }

  public getActivityList() {
    return this.client.get<Array<ActivityListModel>>(`${Globals.DATA_ACCESS_PREFIX}/activity`);
  }

  public createNewActivity(activity: ActivityInsertModel){
    return this.client.post(`${Globals.DATA_ACCESS_PREFIX}/activity/new`, activity);
  }

  public getActivity(activityId: number){
    return this.client.get<ActivityModel>(`${Globals.DATA_ACCESS_PREFIX}/activity/${activityId}`);
  }

  public getSignedUpActivity(activityId: number){
    return this.client.get<ActivitySignedUpViewModel>(`${Globals.DATA_ACCESS_PREFIX}/activity/${activityId}/signed-up`);
  }

  public createNewPerson(person: PersonInsertModel)
  {
    return this.client.post(`${Globals.DATA_ACCESS_PREFIX}/person`, person);
  }

  public createNewComment(comment: CommentInsertModel)
  {
    return this.client.post(`${Globals.DATA_ACCESS_PREFIX}/comment`, comment);
  }
}
