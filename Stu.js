import { Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/Button";


function Stu() {
    const [Data, setdata] = useState([]);
    const navigate = useNavigate();

    const [showModal, setShowModal] = useState(false);
    const [itemToDeleteId, setItemToDeleteId] = useState(0);
   
    useEffect(() => {
      axios.get("https://localhost:44375/api/Teach").then((response) => {
        setdata((data) => {
          return response.data;
        });
      });
    }, []);

   
   return (
      <>
      <Row className="mt-2">
        <Col md={{ span: 4, offset: 4 }}>
          <Button
            variant="primary"
            type="button"
            onClick={() => {
              navigate("/student-create");
            }}
          >
            Add Student
          </Button>
        </Col>
      </Row>

      
        <Row md={3} className="g-4 mt-1">
          {Data.map((da) => {
            return (
              <Col key={da.id}>
                <Card>
                    
                  <Card.Img variant="top" src={da.empImage}  />
                  
                  <Card.Body>
                  <Card.Text>
                      <b>Student Id:</b> {da.stuId}
                    </Card.Text>
                  <Card.Text>
                      <b>First Name:</b> {da.firstName}
                    </Card.Text>
                    <Card.Text>
                      <b>Last Name:</b> {da.lastName}
                    </Card.Text>
                   <Card.Text>
                      <b>Gender:</b> {da.gender}
                    </Card.Text>
                    <Card.Text>
                      <b>Date Of Birth:</b> {da.dob}
                    </Card.Text>
                    <Card.Text>
                      <b>Date Of Joining:</b> {da.doj}
                    </Card.Text>

                  <Button
                    variant="primary"
                    onClick={() => navigate(`/student-update/${da.id}`)}>
                    Edit
                  </Button>
                    
                  </Card.Body>
                </Card>
              </Col>
            );
          })}
        </Row>
      </>
    );
  }
   

  export default Stu;