import * as React from "react";
import { RouteComponentProps } from "react-router";
import { PropTypes } from "react";
import { NavLink } from "react-router-dom";

export class NewsFeedItem extends React.Component<
  any & RouteComponentProps<{}>,
  {}
> {
  static PropTypes = {
    data: PropTypes.any
  };
  public render() {
    const {
      title,
      thumbnailURL,
      description,
      link,
      pubDate,
      provider,
      providerId
    } = this.props.data;
    return (
      <div className="card item-card">
        <a
          href={link}
          style={{ textDecoration: "none", color: "#333" }}
          target="_blank"
        >
          <h3> {title}</h3>
          <span>
            {new Date(pubDate).toLocaleDateString() +
              " " +
              new Date(pubDate).toLocaleTimeString()}
            - <NavLink to={`/provider/${providerId}`}>{provider}</NavLink>
          </span>
          <div className="item-details">
            <img 
              src={thumbnailURL}
              alt="feed-item-thumbnail"
              height="100px"
              width="100px"
            />

            <p> {description}</p>
          </div>
        </a>
      </div>
    );
  }
}
