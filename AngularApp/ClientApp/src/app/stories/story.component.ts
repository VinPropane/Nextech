import { Component, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
//import { MatFormField } from '@angular/material/form-field';
//import { MatInput } from '@angular/material/input';
import { Story } from './story';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
})

export class StoryComponent {
  stories: MatTableDataSource<Story>;
  public displayedColumns: string[] = ['id', 'title', 'url'];
  public isLoading = false;
  public filter = '';
  @ViewChild(MatPaginator) paginator: MatPaginator;
  pageEvent: PageEvent;

  constructor(private http: HttpClient) {
    this.cacheStories();
  }

  cacheStories() {
    var url = 'http://localhost:5000/api/story/GetNewStoriesAsync';
    this.getResults(url, new HttpParams);
  };

  getStories(event: PageEvent) {
    var url = `http://localhost:5000/api/story/GetStories/${event.pageIndex.toString()}/${event.pageSize.toString()}`;
    this.getResults(url, new HttpParams().set("searchFilter", this.filter));
    return event;
  }

  getFilteredStories(filter: string) {
    var url = 'http://localhost:5000/api/story/GetStories/0/50'
    this.filter = filter;
    var httpParams = new HttpParams().set("searchFilter", filter);
    this.getResults(url, httpParams);
  }

  getResults(url: string, params: HttpParams) {
    this.isLoading = true;
    this.http.get<any>(url, { params }).subscribe(response => {
      this.stories = new MatTableDataSource(response.stories);
      this.paginator.length = response.storyCount;
      this.paginator.pageIndex = response.pageIndex;
      this.paginator.pageSize = response.pageSize;
      this.isLoading = false;
    });
  }

  ngOnInit() {
    if (this.stories) {
      this.stories.paginator = this.paginator;
    }
  }
}
