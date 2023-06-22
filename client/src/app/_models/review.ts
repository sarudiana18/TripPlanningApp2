export interface Review{
    descriereReview?: string;
    titlu?:string;
    nota?: number;
    created_At: Date;

    createdAtString?: string;
    hotelId?: number;
    parcId?: number;
    restaurantId?: number;
    createdBy: number;
    createdByNume?: string;
    id?: number;
}