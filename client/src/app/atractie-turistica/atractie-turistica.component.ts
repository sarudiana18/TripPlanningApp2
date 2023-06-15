import { Component, Input, OnInit } from '@angular/core';
import { AtractieTuristica } from '../_models/atractieturistica';

@Component({
  selector: 'app-atractie-turistica',
  templateUrl: './atractie-turistica.component.html',
  styleUrls: ['./atractie-turistica.component.css']
})
export class AtractieTuristicaComponent implements OnInit {

  @Input() atractie: AtractieTuristica | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
