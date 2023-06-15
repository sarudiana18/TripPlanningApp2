import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { City } from "./city";
import { Country } from "./country";
import { State } from "./state";

export class TripParams {
    orderBy = 'nume';
    pageNumber = 1;
    pageSize = 5;
    destinationCity!: City;
    destinationState!: State;
    destinationCountry!: Country;

    sourceCity!: City;
    sourceState!: State;
    sourceCountry!: Country;
    nrPersoane = 0;
    buget = 0;
    startDate = '';
    endDate = '';
    nrNopti = 0;

}