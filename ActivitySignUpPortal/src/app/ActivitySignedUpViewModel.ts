import { Deserializable } from './deserializable.model';
import { PersonListModel } from "./PersonListModel";
import { CommentListModel } from "./CommentListModel";

export class ActivitySignedUpViewModel implements Deserializable {
     ActivityId: number | undefined;
     ActivityName: string | undefined;
     ActivityDescription: string | undefined;
     ActivityDateTime: Date | undefined;
     ActivityImage: string | undefined;
     ParticipantList: Array<PersonListModel> | undefined;
     CommentList: Array<CommentListModel> | undefined;

    constructor(
 
    ){}

    deserialize(input: any): this {
        Object.assign(this, input);
        for(let participant of input.participantList)
        {
            this.ParticipantList?.push(new PersonListModel().deserialize(participant));
        }
        for(let comment of input.commentList)
        {
            this.CommentList?.push(new CommentListModel().deserialize(comment));
        }
        return this;
    }


}