import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirm-password',
  templateUrl: './confirm-password.component.html',
  styleUrls: ['./confirm-password.component.css']
})
export class ConfirmPasswordComponent implements OnInit {
  fromLink: boolean
  mail: any;
  token: any;
  name: string;
  lName: string;

  constructor(private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe(params => {
      this.mail = params.mail;
      this.token = params.token;
      this.name = params.name;
      this.lName = params.lName;
      if (this.mail !== undefined) {
        this.fromLink = true;
      }
    });

    console.log(this.mail);
    console.log(this.token);
  }

}
