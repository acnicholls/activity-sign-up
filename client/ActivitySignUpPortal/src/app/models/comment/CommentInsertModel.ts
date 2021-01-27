export class CommentInsertModel {

    constructor(
        public CommentPersonId: number,
        public CommentActivityId: number,
        public CommentContent: string
    ){}
}