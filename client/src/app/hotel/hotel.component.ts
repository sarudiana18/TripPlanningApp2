import { Component, Input, OnInit } from '@angular/core';
import { Hotel } from '../_models/hotel';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.css']
})
export class HotelComponent implements OnInit {

  @Input() hotel: Hotel | undefined;
  @Input() startDate: string | undefined;
  @Input() endDate: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
