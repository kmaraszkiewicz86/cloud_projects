import { Component, OnInit } from '@angular/core';

import { DynamoDbServiceService } from '../../services/dynamo-db-service.service'

@Component({
  selector: 'dynamo-db-add',
  templateUrl: './dynamo-db-add.component.html',
  styleUrls: ['./dynamo-db-add.component.css']
})
export class DynamoDbAddComponent implements OnInit {

  errorMessage: string = ""

  get isErrorExists(): boolean {
    return this.errorMessage != ""
  }

  newNameItem: string = ""

  constructor(private dynamoDbServiceService : DynamoDbServiceService) { }

  ngOnInit(): void {
  }

  public addNewItem() {
    if (this.newNameItem != '') {

      this.dynamoDbServiceService.add({ name: this.newNameItem }).subscribe({
        error: err => this.errorMessage = err
      });

      this.newNameItem = '';
    }
  }

}
