function Z=PrepareData(X1,T, Obj, Meter )
    % ״̬X=[x1,x2]  λ��x1���ٶ�x2
    % ״̬���� X(t) = phi*X(t-1)+ B*u(t-1)+gamma*e1(t-1);
    %      phi=[1 1; 0,1] 
    %      B=[1^2/2; T]    u(t)���ٶ�
    %      gamma=B;        e1(t)ϵͳ����������Q�� 
    % �������� Z(t)=H*X(t)+e2(t);
    %         H=[1;0];
    %         e2(t)��������������R��

    % 1 ģ��Obj�����й��̣�����X
	e1=normrnd(0, Obj.Q, 1,T);  % ϵͳ����
    X(:,1)=X1;
    for t=2:T
        X(:,t)=Obj.phi*X(:,t-1)+Obj.B*Obj.us(t-1)+Obj.gamma*e1(t-1);
    end
    % 2 ģ��۲��¼,����Z
    e2=normrnd(0,Meter.R,1,T); % �۲�����
    Z=X(1,:)+e2;
end
