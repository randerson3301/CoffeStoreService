import { Component, Input, OnInit } from '@angular/core';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faCircleCheck } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-success-alert',
  templateUrl: './success-alert.component.html',
  styleUrls: ['./success-alert.component.css']
})
export class SuccessAlertComponent implements OnInit {

  @Input() showWhen: boolean = false;
  @Input() successMessage: string = "";
  public faCircleCheck: IconDefinition = faCircleCheck;

  constructor() { }

  ngOnInit(): void {
  }

}
