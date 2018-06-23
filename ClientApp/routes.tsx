import * as React from "react";
import { Route, NavLink } from "react-router-dom";
import { Layout } from "./components/Layout";
import { NewsFeedList } from "./components/NewsFeedList";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import { NewsProvidersList } from "./components/NewsProvidersList";

export const routes = (
  <Layout>
    <Route
      exact
      path="/"
      render={() => (
        <NewsFeedList
          onlySubscribed
          renderIfNoItemsFound={() => (
            <div className="text-center vertically-centered-content font-x-large">
              <p>Welcome <span > 🎉</span>,</p>
              <p>
                Subscribe some of the {" "}
                <NavLink to={"/providers"} activeClassName="active">
                  News Providers 📰
                </NavLink>
                {" "} to make this Home, <i>your own </i> 🏠.
              </p>
              <p>
                You can always check feeds from all providers at {" "}
                <NavLink to={"/all-feeds"} activeClassName="active">
                  All Feeds 📚
                </NavLink>
              </p>
            </div>
          )}
        />
      )}
    />
    <Route exact path="/all-feeds" render={() => <NewsFeedList />} />
    <Route
      exact
      path="/provider/:id"
      render={(props: any) => (
        <NewsFeedList providerId={props.match.params.id} />
      )}
    />
    <Route path="/providers" component={NewsProvidersList} />
    <Route path="/fetchdata" component={FetchData} />
  </Layout>
);
