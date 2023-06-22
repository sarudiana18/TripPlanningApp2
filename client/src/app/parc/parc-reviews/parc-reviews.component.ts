import { Component, Input, OnInit } from '@angular/core';
import { Review } from 'src/app/_models/review';
import { TripPlanningService } from '../../_services/tripplanning.service';

@Component({
  selector: 'app-parc-reviews',
  templateUrl: './parc-reviews.component.html',
  styleUrls: ['./parc-reviews.component.css']
})
export class ParcReviewsComponent implements OnInit {

  @Input() parcId = 0;
  @Input() reviews: Review[] = [];

  displayForAddingNewReview: boolean = false;
  newReview: Review = {
    createdBy: 0,
    descriereReview: '',
    nota: 0,
    titlu:'',
    created_At: new Date(),
    createdAtString:'',
    createdByNume:'',
    parcId: this.parcId,
  };
  constructor(private tripPlanningService: TripPlanningService) { }

  ngOnInit(): void {
    // this.tripPlanningService.getReviewsByParcId(this.parcId).subscribe({
    //   next: response => {
    //     this.reviews = response;
    //   }
    // })
    if(this.reviews.length > 0){
      this.reviews.forEach(x=>{
        x.createdAtString = this.convertDateTimeToString(x.created_At)
      })
    }
  }

  convertDateTimeToString(data: any){
    const date = new Date(data);

    const year = date.getFullYear();
    const month = date.getMonth() + 1; // Adaugăm 1, deoarece indexul lunilor începe de la 0
    const day = date.getDate();

    const dateOnlyString = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
    return dateOnlyString;
  }

  addReview(){
    this.displayForAddingNewReview = true;
  }
  submitReview(){
    this.newReview.created_At = new Date();
    
    // Add the new review to the reviews array
    this.reviews.push(this.newReview);
    
    // Clear the form fields
    this.newReview = {
      createdBy: 0,
      descriereReview: '',
      nota: 0,
      titlu:'',
      created_At: new Date(),
      createdAtString:'',
      createdByNume:'',
      parcId: this.parcId,
    }
  }
}
