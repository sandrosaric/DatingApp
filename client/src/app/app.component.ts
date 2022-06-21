import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  users:any;
  baseUrl = "https://localhost:5001/api/users";

  constructor(private http:HttpClient) {


  }

  ngOnInit(){
    this.getUsers();
    }


    getUsers(){
      this.http.get(this.baseUrl).subscribe(
        {
          next:result => this.users = result,
          error:error => console.log(error)
        }
      )

}
}
