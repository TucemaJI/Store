import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { IConfirmModel } from '../../models/IConfirmModel';
import { confirmPassword } from '../../store/account.actions';

@Component({
  selector: 'app-confirm-password',
  templateUrl: './confirm-password.component.html',
  styleUrls: ['./confirm-password.component.css']
})
export class ConfirmPasswordComponent implements OnInit {
  fromLink: boolean
  mail: string;
  token: any;
  name: string;
  lName: string;
  confirmModel: IConfirmModel;
  password: string;
  constructor(private store: Store, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe(params => {
      this.mail = params.mail;
      this.token = decodeURIComponent(params.token);
      this.name = params.name;
      this.lName = params.lName;
      this.password = params.pass;
      this.confirmModel = { email: this.mail, token: this.token, password: this.password }
      if (this.mail !== undefined) {
        this.fromLink = true;
      }
    });

    console.log(this.mail);
    console.log(this.token);
  }
  public submit(confirmModel) {

    this.store.dispatch(confirmPassword(confirmModel));
    console.log(this.token);
    debugger;

  }
}
