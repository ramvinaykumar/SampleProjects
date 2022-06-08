import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: [
  ]
})
export class DashboardComponent implements OnInit {

  public userList: any[];

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.httpClient.get<any>("http://localhost:3000/registerUsers")
      .subscribe(res => {
        this.userList = res;
        console.log(this.userList);
      }, err => {
        console.log(err);
      })
  }

}