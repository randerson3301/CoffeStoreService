import { Component, Input, OnInit } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faCircleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-input-errors',
  templateUrl: './input-errors.component.html',
  styleUrls: ['./input-errors.component.css']
})
export class InputErrorsComponent implements OnInit {

  @Input() errorMessages: string[] = [];
  faCircleExclamation: IconDefinition = faCircleExclamation;

  constructor() { }

  ngOnInit(): void {
  }

}
