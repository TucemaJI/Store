import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngxs/store';
import { IPay } from 'src/app/modules/shared/models/IPay.model';
import { Pay } from '../../store/cart.actions';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  public paymentForm: FormGroup;

  constructor(private store: Store, public dialogRef: MatDialogRef<PaymentComponent>, @Inject(MAT_DIALOG_DATA) public data: { total: number, orderId: number }) { }

  ngOnInit(): void {
    this.paymentForm = new FormGroup({
      cardnumber: new FormControl('', [Validators.required, Validators.maxLength(16), Validators.minLength(16), Validators.pattern('[0-9]{16}')],),
      month: new FormControl('', [Validators.required, Validators.min(1), Validators.max(12)]),
      year: new FormControl('', [Validators.required, Validators.min(0), Validators.max(9999)]),
      cvc: new FormControl('', [Validators.required, Validators.min(0), Validators.max(999),]),
    });
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.paymentForm.controls[controlName].hasError(errorName);
  }

  public submit(paymentFormValue: IPay): void {
    const payment: IPay = {
      cardnumber: paymentFormValue.cardnumber,
      cvc: paymentFormValue.cvc,
      month: paymentFormValue.month,
      year: paymentFormValue.year,
      orderId: this.data.orderId,
      value: this.data.total,
    }
    this.store.dispatch(new Pay(payment));
    this.dialogRef.close();
  }
}
