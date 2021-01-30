export class ActivityInsertModel {
    public ActivityImage: string | undefined;
    constructor(
        public ActivityName: string,
        public ActivityDescription: string,
        public ActivityDateTime: Date,
    ){}
}