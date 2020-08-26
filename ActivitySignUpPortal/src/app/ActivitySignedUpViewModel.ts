import { Deserializable } from './deserializable.model';
import { PersonListModel } from "./PersonListModel";
import { CommentListModel } from "./CommentListModel";

export class ActivitySignedUpViewModel implements Deserializable {
     activityId: number | undefined;
     activityName: string | undefined;
     activityDescription: string | undefined;
     activityDateTime: Date | undefined;
     activityImage: string | undefined;
     participantList: Array<PersonListModel> | undefined;
     commentList: Array<CommentListModel> | undefined;

    constructor(
 
    ){}

    deserialize(input: any): this {
        Object.assign(this, input);
     //   for(let participant of input.participantList)
     //   {
     //       this.participantList?.push(new PersonListModel().deserialize(participant));
     //   }
     //   for(let comment of input.commentList)
     //   {
     //       this.commentList?.push(new CommentListModel().deserialize(comment));
     //   }
        return this;
    }


}