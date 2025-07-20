import { Card, message, Spin } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import StudentForm from "../components/StudentForm";
import api from "../services/api";

export default function EditStudent() {
  const navigate = useNavigate();
  const { id } = useParams();
  const [studentData, setStudentData] = useState({});
  const [loading, setLoading] = useState(true);
  const [messageApi, contextHolder] = message.useMessage();

  useEffect(() => {
    const fetchStudent = async () => {
      try {
        const response = await api.get(`/api/v1/students/${id}`);
        console.log(response.data.result.result)
        setStudentData(response.data.result.result);
      } catch (error) {
        messageApi.error("Error fetching student data: " + error.message);
      } finally {
        setLoading(false);
      }
    };
    fetchStudent();
  }, [id, messageApi]);

  const onSuccess = () => {
    messageApi.success("Student updated successfully.");
    navigate("/students");
  };

  const onCancel = () => {
    navigate("/students");
  };

  return (
    <>
      {contextHolder}
      <Spin spinning={loading} fullscreen="true" />
      <Card title="Edit Student">
        <StudentForm
          initialValues={studentData}
          onSuccess={onSuccess}
          onCancel={onCancel}
        />
      </Card>
    </>
  );
}

