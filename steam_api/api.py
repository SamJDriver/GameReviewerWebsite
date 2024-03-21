from flask import Flask
import json
import mariadb

app = Flask(__name__)

config = {
    'host': '',
    'port': 3306,
    'user': '',
    'password': '',
    'database': 'db'
}


@app.route('/getTest', methods=['GET'])
def getTest():
    conn = mariadb.connect(**config)
    # create a connection cursor
    cur = conn.cursor()
    # execute a SQL statement
    cur.execute("select * from test_game")

    # serialize results into JSON
    row_headers=[x[0] for x in cur.description]
    rv = cur.fetchall()
    json_data=[]
    for result in rv:
            json_data.append(dict(zip(row_headers,result)))

    # return the results!
    return json.dumps(json_data)

@app.route('/getUsers', methods=['GET'])
def getUsers():
    return ""