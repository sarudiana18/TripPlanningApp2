import { Component, Input, OnInit } from '@angular/core';
import { Parc } from '../_models/parc';

@Component({
  selector: 'app-parc',
  templateUrl: './parc.component.html',
  styleUrls: ['./parc.component.css']
})
export class ParcComponent implements OnInit {

  @Input() parc: Parc | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
