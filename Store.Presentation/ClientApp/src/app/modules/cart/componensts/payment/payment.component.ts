import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { IPayModel } from 'src/app/modules/shared/models/IPayModel';
import { IAppState } from 'src/app/store/state/app.state';
import { pay } from '../../store/cart.actions';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  paymentForm: FormGroup;

  constructor(private store: Store<IAppState>, public dialogRef: MatDialogRef<PaymentComponent>, @Inject(MAT_DIALOG_DATA) public data: { total: number, orderId: number }) { }

  ngOnInit(): void {
    this.paymentForm = new FormGroup({
      cardNumber: new FormControl('', [Validators.required, Validators.maxLength(16), Validators.minLength(16)]),
      month: new FormControl('', [Validators.required, Validators.min(1), Validators.max(12)]),
      year: new FormControl('', [Validators.required, Validators.min(0), Validators.max(99)]),
      cvc: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]),
    });
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.paymentForm.controls[controlName].hasError(errorName);
  }

  public submit(paymentFormValue) {
    const payment: IPayModel = {
      cardnumber: paymentFormValue.cardNumber,
      cvc: paymentFormValue.cvc,
      month: paymentFormValue.month,
      year: paymentFormValue.year,
      orderId: this.data.orderId,
      value: this.data.total,
    }
    this.store.dispatch(pay(paymentFormValue));
    console.log(paymentFormValue);
    debugger;
  }

}
