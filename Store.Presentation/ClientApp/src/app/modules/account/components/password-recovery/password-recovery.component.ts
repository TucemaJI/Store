import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { PasswordRecovery } from '../../store/account.actions';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {
  public emailForm: FormGroup;
  public passwordSent: boolean;

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.emailForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.emailForm.controls[controlName].hasError(errorName);
  }

  public continue(email: { email: string }): void {
    if (this.emailForm.valid) {
      this.store.dispatch(new PasswordRecovery(email));
      this.passwordSent = true;
    }
  }

}
