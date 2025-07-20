import { useEffect, useState } from "react";
import api from "../services/api";
import { message, Spin, Table, Card, Button, Space } from "antd";
import { useNavigate } from "react-router-dom";

export default function Students() {
    const [loading, setLoading] = useState(false);
    const [students, setStudents] = useState([]);
    const [messageApi, contextHolder] = message.useMessage();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchStudents = async () => {
            try {
                setLoading(true);
                const response = await api.get("/api/v1/students");
                if (response.status === 200) {
                    messageApi.success("Students data fetched successfully.");
                    setStudents(response.data.result.result);
                    console.log(response.data);
                } else {
                    messageApi.error("Failed to fetch students data.");
                    console.log(response.data);
                }
            } catch (error) {
                messageApi.error(
                    "Error fetching students data: " + error.message
                );
                console.log(error);
            } finally {
                setLoading(false);
            }
        };
        fetchStudents();
    }, [messageApi]);

    const handleAddStudent = () => {
        navigate("/students/create");
    };

    const handleEditStudent = (studentId) => {
        navigate(`/students/${studentId}/edit`);
    };

    const handleDeleteStudent = (studentId) => {
        navigate(`/students/${studentId}/delete`);
    };

    const columns = [
        {
            title: "ID",
            dataIndex: "id",
            key: "id",
        },
        {
            title: "Name",
            dataIndex: "name",
            key: "firstName",
        },
        {
            title: "Email",
            dataIndex: "email",
            key: "email",
        },
        {
            title: "Actions",
            key: "actions",
            render: (_, record) => (
                <Space size="middle">
                    <Button
                        type="primary"
                        size="small"
                        onClick={() => handleEditStudent(record.id)}
                    >
                        Edit
                    </Button>
                    <Button
                        type="primary"
                        danger
                        size="small"
                        onClick={() => handleDeleteStudent(record.id)}
                    >
                        Delete
                    </Button>
                </Space>
            ),
        },
    ];
    return (
        <>
            {contextHolder}
            <Card
                title="Students"
                extra={
                    <Button
                        type="primary"
                        onClick={handleAddStudent}
                    >
                        Add Student
                    </Button>
                }
            >
                <Spin spinning={loading}>
                    <Table
                        dataSource={students}
                        columns={columns}
                        rowKey="id"
                        pagination={{ pageSize: 10 }}
                    />
                </Spin>
            </Card>
        </>
    );
}
