import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    lastName: string;
    created: Date;
    lastActive: Date;
    firstName: string;
    city: string;
    country: string;
    photos: Photo[];
}