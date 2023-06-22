import { Component, Input, OnInit } from '@angular/core';
import { Restaurant } from '../_models/restaurant';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit {

  @Input() restaurant: Restaurant | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
