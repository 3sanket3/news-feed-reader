import * as React from "react";
import { RouteComponentProps } from "react-router";
import { NewsFeedItem } from "./NewsFeedItem";
import { NavLink } from "react-router-dom";

export class NewsProvidersList extends React.Component<
  RouteComponentProps<{}>,
  {}
> {
  state = { providers: new Array<Provider>() };
  componentDidMount() {
    this.getAllProviders();
  }

  getAllProviders = () => {
    fetch("api/Provider/Get")
      .then(response => response.json() as Promise<Provider[]>)
      .then(data => {
        this.setState({ providers: data });
      });
  };

  updateSubscription = (providerId: number, subsribe: boolean) => {
    fetch("api/Provider/Put?id=" + providerId + "&subscribe=" + subsribe, {
      method: "PUT"
    })
      .then(response => response.json() as Promise<Provider[]>)
      .then(data => {
        this.setState({ providers: data });
      });
  };
  public render() {
    console.log(this.state.providers);
    return (
      <div>
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Provider</th>
              <th>Subsciption</th>
            </tr>
          </thead>
          <tbody>
            {this.state.providers
              ? this.state.providers.map((provider, index) => (
                  <tr key={index}>
                    <td>
                      <NavLink to={`/provider/${provider.id}`}>
                        {provider.name}
                      </NavLink>
                    </td>
                    <td>
                      {provider.isSubscribed ? (
                        <button
                          onClick={() =>
                            this.updateSubscription(provider.id, false)
                          }
                        >
                          Unsubscribe
                        </button>
                      ) : (
                        <button
                          onClick={() =>
                            this.updateSubscription(provider.id, true)
                          }
                        >
                          Subscribe
                        </button>
                      )}
                    </td>
                  </tr>
                ))
              : null}
          </tbody>
        </table>
      </div>
    );
  }
}

export interface Provider {
  name: string;
  isSubscribed: boolean;
  logo: string;
  id: number;
}
