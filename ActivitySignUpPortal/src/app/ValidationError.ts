export class ValidationError {

    constructor(
        public FieldName: string,
        public ErrorDetail: string
    ){}
}