describe('Visitor Counter Test', () => {
  it('increments after refresh', () => {
    let initialCount;
    cy.request('https://resumecountercrc-python.azurewebsites.net/api/VisitorCounter')
      .then((response) => {
        initialCount = parseInt(response.body);
        expect(initialCount).to.be.a('number');
      });

    cy.reload();

    cy.request('https://resumecountercrc-python.azurewebsites.net/api/VisitorCounter')
      .then((response) => {
        const updatedCount = parseInt(response.body);
        expect(updatedCount).to.equal(initialCount + 1);
      });
  });
});

