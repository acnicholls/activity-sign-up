export class ActivityInsertModel {
    constructor(
        public ActivityName: string,
        public ActivityDescription: string,
        public ActivityDateTime: Date,
        public ActivityImage: string
    ){}
}