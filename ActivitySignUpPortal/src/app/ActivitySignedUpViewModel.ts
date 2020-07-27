import { PersonListModel } from "./PersonListModel";
import { CommentListModel } from "./CommentListModel";

export class ActivitySignedUpViewModel {
     ActivityId: number | undefined;
     ActivityName: string | undefined;
     ActivityDescription: string | undefined;
     ActivityDateTime: Date | undefined;
     ActivityImage: string | undefined;
     ParticipantList: Array<PersonListModel> | undefined;
     CommentList: Array<CommentListModel> | undefined;

    constructor(
 
    ){}
}