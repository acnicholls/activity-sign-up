import { Deserializable } from '../deserializable.model';
import { PersonListModel } from "../person/PersonListModel";
import { CommentListModel } from "../comment/CommentListModel";

export class ActivitySignedUpViewModel implements Deserializable {
     activityId: number | undefined;
     activityName: string | undefined;
     activityDescription: string | undefined;
     activityDateTime: Date | undefined;
     activityImage: File | undefined;
     participantList: Array<PersonListModel> | undefined;
     commentList: Array<CommentListModel> | undefined;

    constructor(
 
    ){}

    deserialize(input: any): this {
        Object.assign(this, input);
         return this;
    }


}