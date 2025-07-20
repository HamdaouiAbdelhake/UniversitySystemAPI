import { Button, Card, message, Modal, Spin, Typography, Space } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { ExclamationCircleOutlined, DeleteOutlined, ArrowLeftOutlined } from "@ant-design/icons";
import api from "../services/api";

const { Title, Text } = Typography;
const { confirm } = Modal;

export default function DeleteStudent() {
  const navigate = useNavigate();
  const { id } = useParams();
  const [deleting, setDeleting] = useState(false);
  const [messageApi, contextHolder] = message.useMessage();


  const handleDelete = async () => {
    try {
      setDeleting(true);
      await api.delete(`/api/v1/students/${id}`);
      messageApi.success("Student deleted successfully.");
      navigate("/students");
    } catch (error) {
      messageApi.error("Error deleting student: " + error.message);
    } finally {
      setDeleting(false);
    }
  };

  const onCancel = () => {
    navigate("/students");
  };


  return (
    <>
      {contextHolder}
      <Card title="Delete Student">
        <Space direction="vertical" size="large" style={{ width: '100%' }}>
          
          <Text type="warning" style={{ fontSize: '16px' }}>
            ⚠️ Are you sure you want to delete this student? This action cannot be undone.
          </Text>
          
          <Space>
            <Button
              type="primary"
              danger
              icon={<DeleteOutlined />}
              onClick={handleDelete}
              loading={deleting}
              size="large"
            >
              Delete Student
            </Button>
            <Button
              icon={<ArrowLeftOutlined />}
              onClick={onCancel}
              size="large"
            >
              Cancel
            </Button>
          </Space>
        </Space>
      </Card>
    </>
  );
}