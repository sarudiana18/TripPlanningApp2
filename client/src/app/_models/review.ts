export interface Review{
    descriereReview?: string;
    titlu?:string;
    nota?: number;
    created_At: Date;

    createdAtString?: string;
    hotelId: number;
    createdBy: number;
    createdByNume?: string;
    id?: number;
}