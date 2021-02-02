import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dynamo-db-details',
  templateUrl: './dynamo-db-details.component.html',
  styleUrls: ['./dynamo-db-details.component.css']
})
export class DynamoDbDetailsComponent implements OnInit {

  id: string

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

}
