import logging
import os

from azure.data.tables import TableClient
from azure.data.tables import UpdateMode

import azure.functions as func

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')
    
    ##Open connection to Azure Cosmos DB table and grab the entity from the table
    connection_string = os.getenv("CosmosDbConnectionString")
    with TableClient.from_connection_string(connection_string, table_name="counter") as table:
        count_entity = table.get_entity('0', '0')
        count_entity = updatecount(count_entity)
    ## Update the entity with new count by replacing the old count.     
        table.update_entity(mode=UpdateMode.REPLACE, entity=count_entity)
    ##Return the count/entity as http response.
    return func.HttpResponse(f"{count_entity['Counter']}")


    '''
    Define updatecount function. Note that the reference to 'Counter' is not a reference to the table_name="counter", but instead 
    a reference to a key-value pair (property) of the entity. You can verify from within the azure cosmos db table by editing the table
    entity and finding property name "Counter" of type string with the value set to the actual visitor counter. 
    '''
def updatecount(count_entity):
    count_entity['Counter'] += 1
    return count_entity



##test