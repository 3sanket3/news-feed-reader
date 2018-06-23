import * as React from "react";
import { RouteComponentProps } from "react-router";
import { NewsFeedItem } from "./NewsFeedItem";

export class NewsFeedList extends React.Component<
  any & RouteComponentProps<{}>,
  {}
> {
  static PropTypes = {
    onlySubscribed: React.PropTypes.bool,
    providerId: React.PropTypes.number,
    renderIfNoItemsFound: React.PropTypes.func
  };

  state = {
    feedItmes: [],
    apiResponded: false /*renderIfNoItemsFound should not be called if api is yet to respond */,
    searchTerm: ""
  };

  backupFeeds: Array<any> = [];
  componentDidMount() {
    console.log("onlySubscribed " + this.props.onlySubscribed);
    this.getAllFeeds();
  }

  getAllFeeds = () => {
    let url = this.props.onlySubscribed
      ? "api/News/Get?onlySubscribed=true"
      : "api/News/Get";

    if (this.props.providerId > 0) {
      url = "api/News/Get?providerId=" + this.props.providerId;
    } else if (this.props.onlySubscribed) {
      url = "api/News/Get?onlySubscribed=true";
    } else {
      url = "api/News/Get";
    }

    fetch(url)
      .then(response => response.json() as Promise<any[]>)
      .then(data => {
        this.setState({ feedItmes: data, apiResponded: true });
        this.backupFeeds = [...this.state.feedItmes];
      });
  };

  handleChange = (event: any) => {
    this.setState({ searchTerm: event.target.value }, () => {
      if (!this.state.searchTerm) {
        this.setState({ feedItmes: this.backupFeeds });
      }
    });
  };
  handleSubmit = (event: any) => {
    let searchedItems = this.backupFeeds.filter(
      feed =>
        (feed.title as string)
          .toLowerCase()
          .indexOf(this.state.searchTerm.toLowerCase()) >= 0
    );
    this.setState({ feedItmes: searchedItems });
    event.preventDefault();
  };
  public render() {
    return (
      <div>
        {this.state.feedItmes && this.state.feedItmes.length ? (
          <div>
            <div className="main-content-header">
              <form onSubmit={this.handleSubmit}>
                <input
                  type="text"
                  value={this.state.searchTerm}
                  onChange={this.handleChange}
                  placeholder="Search"
                />

                <input type="submit" value="Submit" />
              </form>
            </div>
            {this.state.feedItmes.map((feedItem, index) => (
              <NewsFeedItem
                data={feedItem}
                key={index}
                {...{} as RouteComponentProps<any>}
              />
            ))}
          </div>
        ) : (
        
          this.state.apiResponded &&
          !this.state.searchTerm &&
          this.props.renderIfNoItemsFound &&
          this.props.renderIfNoItemsFound()
        )}
      </div>
    );
  }
}
