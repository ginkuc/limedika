export class Log {
    id: number;
    locationId: number;
    date: Date;
    action: LocationActionEnum;
}

export enum LocationActionEnum {
    None = 0,
    Created,
    PostCodeUpdated
}
