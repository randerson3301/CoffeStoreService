import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IconDefinition, faCircleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-form-user-password',
  templateUrl: './form-user-password.component.html',
  styleUrls: ['./form-user-password.component.css']
})
export class FormUserPasswordComponent implements OnInit {
  @Input() userForm: FormGroup | any;
  @Output() passwordEqualityValidator: EventEmitter<boolean> = new EventEmitter<boolean>();

  public password: string = "";
  public confirmPassword: string = "";
  faCircleExclamation: IconDefinition = faCircleExclamation;

  constructor() { }

  ngOnInit(): void {
  }

  isConfirmPasswordEqualsPassword(): boolean {
    const isEqual: boolean = (this.confirmPassword != "" && this.password != "" && this.confirmPassword === this.password);
    this.passwordEqualityValidator.emit(isEqual);
    return isEqual;
  }

}
