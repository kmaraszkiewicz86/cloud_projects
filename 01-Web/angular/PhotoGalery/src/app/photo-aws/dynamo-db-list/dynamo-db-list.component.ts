import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AwsPhotoGalery } from '../../services/aws-photo-galery'
import { DynamoDbServiceService } from '../../services/dynamo-db-service.service'

@Component({
  selector: 'app-dynamo-db-list',
  templateUrl: './dynamo-db-list.component.html',
  styleUrls: ['./dynamo-db-list.component.css']
})
export class DynamoDbListComponent implements OnInit {
  errorMessage: string = ""

  awsPhotoGaleryItems: AwsPhotoGalery[] = []

  get isErrorExists(): boolean {
    return this.errorMessage != ""
  }

  constructor(private service : DynamoDbServiceService) { }

  ngOnInit(): void {
    this.getAll();
  }

  public deleteItem(id: string) {
    this.service.delete({ id: id }).subscribe({
      error: err => this.errorMessage = err
    });
  }

  private getAll() {
    this.service.getAll().subscribe({
      next: data => this.awsPhotoGaleryItems = data,
      error: err => this.errorMessage = err
    })
  }

}
