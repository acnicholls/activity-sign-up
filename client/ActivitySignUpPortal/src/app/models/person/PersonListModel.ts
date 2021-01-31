import { Deserializable } from '../deserializable.model';


export class PersonListModel implements Deserializable{
    personName: string | undefined;
    constructor(

    ){}

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}