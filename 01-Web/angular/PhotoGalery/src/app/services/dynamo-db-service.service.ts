import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { tap, catchError } from 'rxjs/operators'

import { AwsPhotoGalery } from "./aws-photo-galery";
import { DeleteRequest } from '../models/deleteRequest'
import { AddItemRequest } from '../models/addItemRequest'

@Injectable({
  providedIn: 'root'
})
export class DynamoDbServiceService {

  private baseUrl = "http://localhost:5555/api/AwsPhotoGallery"

  constructor(private http : HttpClient) { }

  add(addItemRequest: AddItemRequest) : Observable<{}> {
    return this.http.post(this.baseUrl, addItemRequest).pipe(
      tap(data => console.log(data)),
      catchError(this.catchError)
    )
  }

  delete(deleteRequest: DeleteRequest): Observable<{}> {
    return this.http.request('delete', this.baseUrl, { body: deleteRequest }).pipe(
      tap(data => console.log(data)),
      catchError(this.catchError)
    )
  }

  getAll(): Observable<AwsPhotoGalery[]> {
    return this.http.get<AwsPhotoGalery[]>(this.baseUrl).pipe(
      tap(data => console.log(data)),
      catchError(this.catchError)
    )
  }

  private catchError(err: HttpErrorResponse) {
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.error.message}`;
    }

    console.log(err)
    return throwError(errorMessage);
  }
}
