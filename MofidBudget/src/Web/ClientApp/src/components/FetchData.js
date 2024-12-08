import React, { Component } from 'react';
import followIfLoginRedirect from './api-authorization/followIfLoginRedirect';
import { BeneficiariesClient } from '../web-api-client.ts';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { beneficiaries: [], loading: true };
  }

  componentDidMount() {
    this.populateBeneficiaryData();
  }

  static renderBeneficiariesTable(beneficiaries) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Title</th>
            <th>Code</th>
            <th>Group</th>
            
          </tr>
        </thead>
        <tbody>
          {beneficiaries.map(beneficiary =>
            <tr key={beneficiary.id}>
              {/*<td>{new Date(forecast.date).toLocaleDateString()}</td>*/}
              <td>{beneficiary.title}</td>
              <td>{beneficiary.code}</td>
              <td>{beneficiary.beneficiaryGroup}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderBeneficiariesTable(this.state.beneficiaries);

    return (
      <div>
        <h1 id="tableLabel">Beneficiary</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateBeneficiaryData() {
    let client = new BeneficiariesClient();
    const data = await client.getBeneficiaries();
    console.log(Array.isArray(data.items)); // Should log true
    this.setState({ beneficiaries: data.items, loading: false });
  }

  async populateBeneficiaryDataOld() {
    const response = await fetch('beneficiary');
    followIfLoginRedirect(response);
    const data = await response.json();

    this.setState({ beneficiaries: data.items, loading: false });
  }
}
