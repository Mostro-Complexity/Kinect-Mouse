function Ks = stdkalman(Z,T, Obj, Meter)
%  ģ�͵Ļ�������1�� ״̬ת��Obj.phi  Obj.B  Obj.gamma
%  ģ�͵Ļ�������2�� ���ٶ�����Obj.us  ��ǰ���ٶ�Obj.u  
%  ģ�͵Ļ�������3�� ���ٶȵ���������Obj.Q
%  �������ߵ����ԣ�  ����ϵ��Meter.H    �������ķ���Meter.R  
%  �������˲�������  ״̬����K.x   ������ƾ���K.p   ����������K.K
    % 1 ��ʼ״̬
    Ks=[];
    K.x=[Z(1);(Z(2)-Z(1))];    K.p=1000*eye(2);
    % 2 ����
    for t=2:T
        Obj.u=Obj.us(:,t);  % ��ǰ������
        newK = stdkalman1(Z(t), Obj, Meter, K); % ��ǰ����ֵZ(t)
        Ks=[Ks newK];
    end
end

function newK = stdkalman1(z, Obj, Meter,  K)
    % 1 ������Ƶ�״ֵ̬
    X=Obj.phi*K.x+Obj.B*Obj.u;
    P=Obj.phi*K.p*Obj.phi'+Obj.gamma*Obj.Q*Obj.gamma';  
    % 2 �����µ��˲�����
    newK.K=P*Meter.H'* inv( Meter.H*P*Meter.H'+Meter.R );
    newK.X=X+newK.K*( z-Meter.H*X );
    n=length(X);
    newK.P=(eye(n)-newK.K*Meter.H)*P*(eye(n)-newK.K*Meter.H)'+newK.K*Meter.R*newK.K';
end
